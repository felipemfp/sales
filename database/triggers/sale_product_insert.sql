use Sales

create trigger tSaleProductInsert
on SaleProduct
for insert
as
begin
  declare
    @ProductId int,
    @Quantity int,
    @Stock int

  select @ProductId = ProductId, @Quantity = Quantity
  from inserted

  select @Stock = Stock
  from Product
  where Id = @ProductId

  if (@Stock >= @Quantity)
    set @Stock = @Stock - @Quantity

    exec spUpdateProductStock @ProductId, @Stock
  else
    raiserror('Product stock has been exceeded | Product %i', 16, 1, @ProductId)
    rollback transaction
    return
end
