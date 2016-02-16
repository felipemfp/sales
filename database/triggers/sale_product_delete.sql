use Sales

create trigger tSaleProductDelete
on SaleProduct
for delete
as
begin
  declare
    @ProductId int,
    @Quantity int,
    @Stock int

  select @ProductId = ProductId, @Quantity = Quantity
  from deleted

  select @Stock = Stock
  from Product
  where Id = @ProductId

  set @Stock = @Stock + @Quantity
  exec spUpdateProductStock @ProductId, @Stock
end
