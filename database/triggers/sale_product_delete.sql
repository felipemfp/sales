use Sales

create trigger tSaleProductDelete
on SaleProduct
for delete
as
begin
  declare
    @ProductId int,
    @Quantity int

  select @ProductId = ProductId, @Quantity = Quantity
  from deleted

  exec spAddProductStock @ProductId, @Quantity
end
