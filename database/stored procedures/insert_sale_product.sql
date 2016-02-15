use Sales

create procedure spInsertSaleProduct
  @SaleId int,
  @ProductId int,
  @Quantity int
as
begin
  declare
    @Price money,
    @ItemId int

  select @Price = Price from Product where Id = @ProductId
  select @ItemId = max(ItemId) from SaleProduct where SaleId = @SaleId
  set @ItemId = @ItemId + 1

  insert into SaleProduct(SaleId, ItemId, ProductId, Quantity, Price)
  values (@SaleId, @ItemId, @ProductId, @Quantity, @Price)

  -- to do: update total, discount and final total
end
