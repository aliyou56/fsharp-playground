
// raw primitive types -- simple types -- component types (record)
// Wrapper type definition
// type CustomerId = CustomerId of int
// type OrderId = OrderId of int

//               funtion 1
// let customerId = CustomerId 42
// let orderId = OrderId 42
// print "%b" (orderId = customerId) // compiler error

// define a function using a CustomerId
// let processCustomerID (id: CustomerId) = ...

// call it with an OrerId -- compiler error!
// processCustomerId orderId

// deconstruct
// let (CustomerID id) = customerId

// signature: val processCustomerId: CustomerId -> int
// let processCustomerId (CustomerId id) =
//  printfn "id id %s" id

//     name             case label
type WidgetCode       = WidgetCode of string
type UnitQuantity     = UnitQuantity of int
type KilogramQuantity = KilogramQuantity of flaot

// type alias: high performance compare to simple types but loss of type-safety
// type UnitQuantity = int

// value type (struct): still have overhead from the wrapper, but the mem usage will be contiguous in array (cache-friendly)
// [<struct>]
// type UnitQuantity = UnitQuantity of int

// type UnitQuantities = UnitQuantities of int[] // when working with large arrays - best of both worlds

// Modeling with record types
type Order = {
  CustomerInfo : CustomerInfo
  ShippingAddress : ShippingAddress
  BillingAddress : BillingAddress
  OrderLines : OrderLines list
  AmountToBill : BillingAmount
}

// Modeling Unknow Types
type Undefined = exn // exn -> excpetion type
type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BillingAddress = Undefined
type OrderLines = Undefined
type BillingAmount = Undefined

// Modeling with choice Types
type ProductCode =
  | Widget of WidgetCode
  | Gizmo of GizmoCode
type OrderQuantity =
  | Unit of UnitQuantity
  | Kilogram of KilogramQuantity

// Modeling Worflows with Fucntions
type ValidateOrder = UnvalidatedOrder -> ValidatedOrder


// Complex Inputs and Outputs
// output
type PlaceORderEvents = {
  AcknowledgmentSent : AcknowledgmentSent
  OrderPlaced : OrderPlaced
  BillableOrderPlaced : BillableORderPlaced
}
type PlaceOrder = UnvalidatedOrder -> PlaceOrderEvents

// input
type EnvelopeContents = EnvelopeContents of string
type CategorizedMai =
  | Quote of QuoteForm
  | Order of OrderForm
  // etc
type CategorizedInboundMail = EnvelopeContents -> CategorizedMail

type CalculatePrices = OrderForm -> ProductCatalog -> PricedOrder // better with dependencies
// or
type CalculatedPricesInput = {
  OrderForm : OrderForm
  ProductCatalog : ProductCatalog
}
type CalculatePrices = CalculatePricesInput -> PricedOrder // when both inputs are required and strongly connected with each other


// Documented Effects in the functionSignature
type ValidationResponse<'a> = Async<Result<ValidatedOrder, ValidatedError list>>

type ValidateOrder =
  UnvalidatedOrder -> ValidationResponse<ValidatedOrder>
and ValidationError = {
  FieldName : string
  ErrorDescription : string
}

// Identity in DDD terminology: Entities (with a persistent id)  vs Value Objects
type UnpaidInvoice = {
  InvoiceId : InvoiceId
  // other info
}
type PaidInvoice = {
  InvoiceId : InvoiceId
  // other info
}
type Invoice = 
  | Unpaid of UnpaidInvoice
  | Paid of PaidInvoice

let invoice = Paide { InvoiceId ... }
match invoice with
  | Unpaid unpaidInvoice ->
    printfn "The unpaid invoiced is %A" unpaidInvoice.InvoiceId
  | Paid paidInvoice ->
    printfn "The paid invoiced is %A" paidInvoice.InvoiceId

// implementing Equality for Entites
// 1. Override Equals method
// 2. Override GetHashCode method
// 3. Add CustomEquality and NoComparisib attributes to the type to change the fefault behavior
[<CustomEquality; NoComparison>]
type Contact = {
  ContactId : ContactId
  PhoneNumber : PhoneNumber
  EmailAddress : EmailAddress
  }
  with
  override this.Equals(obj) =
    match obj with
    | :? Contact as c -> this.contact = c.ContactId
    | _ -> false
  override this.GetHashCode() =
    hash this.ContactId

// preferable alternative -> disallow equality testing by adding NoEquality type annotation
[>NoEquality; NoComparison>]
type Contact = {
  ContactId : ContactId
  PhoneNumber : PhoneNumber
  EmailAddress : EmailAddress
  }
printfn "%b" (conatct1 = contact2) // compiler error ! -- remove any ambiguity about what equality means at the object level


[<NoEquality; NoComparison>]
type OrderLine = {
  OrderId : OrderId
  ProductId : ProductId
  Qty : int
  }
  with
  member this.Key =
    (this.OrderId, this.ProductId)
printfn "%b" (line1.Key = line2.Key)


