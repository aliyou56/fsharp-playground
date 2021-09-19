# Context: Order-Taking

```
// --------------------------
// Simple types
// --------------------------

// Product codes
data ProductCode = Widget OR GizmoCode
data WidgetCode  = string wtarting with "W" then 4 digits
data GizmoCode = ...

// Order Quantity
data OrderQuantity = UnitQuantity OR kilogramQuantity
data UnitQuantity = ...
data KilogramQuantity


// --------------------------
// Order life cycle
// --------------------------

// ------ unvalidated state --------
data UnvalidatedOrder =
  UnvalidatedCustomerInfo
  AND UnvalidatedShippingAddress
  AND UnvalidatedBillingAddress
  AND list of UnvalidatedOrderLine

data UnvalidatedOrderLine =
  UnvalidatedProductCode
  AND UnvalidatedOrderQuantity

// ----- validated state -----
data ValidatedOrder = ...
data ValidatedOrderLine = ...

// ----- priced state -----
data PricedOrder = ...
data PricedOrderLine = ...

// ----- output events ------
data OrderAcknowledgementSent = ...
data BillableOrderPlaced = ...


// --------------------------
// Workflows
// --------------------------
workflow "Place Order" =
  input: UnvalidatedOrder
  output (on success):
    OrderAcknowledgementSent
    AND OrderPlaced (to send to shipping)
    AND BillableOrderPlaced (to send to billing)
  output (on error):
    InvalidOrder

workflow "Categorized Inbound Mail"
  input: Envelope contents
  outtput:
    QuoteForm (put on appropriate pile)
    OR OrderForm (put on appropriate pile)
    OR ...

workflow "Calculated Prices" =
  input: OrderFrom, ProductCatalog
  output: PricedOrder

// etc
```

