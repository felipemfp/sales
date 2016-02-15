use Sales

create procedure spInsertSaleProduct
  @SaleId int,
  @ProductId int,
  @Quantity int
as
begin
  declare
    @Price money,
    @ItemId int,
    @Total money,
    @Discount money,
    @FinalTotal money,
    @DateSale datetime,
    @DateCompare datetime,
    @ClientId int,
    @VIP bit,
    @Percent int = 10,
    @CountSales int = 0

  select @Price = Price from Product where Id = @ProductId
  select @ItemId = max(ItemId) from SaleProduct where SaleId = @SaleId
  if (@itemId is NULL)
    set @itemId = 0
  set @ItemId = @ItemId + 1

  insert into SaleProduct(SaleId, ItemId, ProductId, Quantity, Price)
  values (@SaleId, @ItemId, @ProductId, @Quantity, @Price * @Quantity)

  select
    @ClientId = ClientId,
    @DateSale = DateSale
  from Sale where Id = @SaleId
  set @DateCompare = dateadd(month, -6, @DateSale)

  select @VIP = VIP from Client where Id = @ClientId

  select @CountSales = count(Id)
  from Sale
  where ClientId = @ClientId
    and DateSale > @DateCompare
    and DateSale < @DateSale

  select @Total = sum(Price) from SaleProduct where SaleId = @SaleId

  if (@Total is NULL)
    set @Total = 0.0

  if (@VIP = 1)
    set @Percent = 15
  else if (@CountSales < 10)
    set @Percent = @CountSales

  set @Discount = @Total * (@Percent / 100)
  set @FinalTotal = @Total - @Discount

  update Sale
  set
    Total = @Total,
    Discount = @Discount,
    FinalTotal = @FinalTotal
  where Id = @SaleId
end
