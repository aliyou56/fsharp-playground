
// module rec Payments = // rec solve the problem
//  ...

module Payments =

  // simple types at the top of the file
  type CardNumber   = CardNumber of int
  type CheckNumber  = CheckNumber of int
  type Currency     = EUR | USD
  type PaymentAmout = PaymentAmount of float

  type CardType = Visa | Mastercard

  // domain types in the middle of the file
  type CreditCardInfo = {
    CardType: CardType
    CardNumber: CardNumber
  }

  type PaymentMethod =
   | Cash
   | Check of CheckNumber
   | Card of CreditCardInfo

  // top-level types at the bottom of the file
  type Payment = {
    Amount: PaymentAmount
    Currency: Currency
    Method: PaymentMethod
  }
  
  type PaymentError =
    | CardTypeNotRecognized
    | PaymentRejected
    | PaymentProviderOffline
  
  // type PayInvoice =
  //  UnpaidInvoice -> Payment -> PaidInvoice
  type PayInvoice =
    UnpaidInvoice -> Payment -> Result<PaidInvoice, PaymentError>
  
  type ConvertPaymentCurrency =
    Payment -> Currency -> Payment


type PersonalInfo = {
  FirstName : string
  MiddleInitial : Option<string> // or string option
  LastName : string
}

// exist in stdlib
type Result<'Success, 'Failure> =
  | OK of 'Success
  | Error of 'Failure



type SaveCustomer = Customer -> unit

type NextRandom = unit -> int

// Lists and  Collectios
type Order = {
  OrderId : OrderId
  Lines : OrderLine list // a collection
}
let aList = [1; 2; 3]
let aNewList = 0 :: aList

let printList aList =
  match aList with
  | [] ->
    printfn "list is empty"
  | [x] -> 
    printfn "list has one elemnt: %A" x
  | [x; y] ->
    printfn "list has two elements: %A and %A" x y
  | longerList ->
    printfn "list has more than two elemnts

let printList2 aList =
  match aList with
  | [] ->
    printfn "list is empty"
  | first::rest ->
    printfn "list is non-empty .."


// Organizing Types in Files and Projects
// Common.Types.fs
// Common.Functions.fs
// OrderTaking.Types.fs
// OrderTaking.Functions.fs
// Shipping.Types.fs
// Shipping.Functions.fs


