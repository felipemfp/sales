use Sales

create trigger tSaleProductInsert
on SaleProduct
for insert
as
begin
  declare
    @ProductId int,
    @Quantity int

  select @ProductId = ProductId, @Quantity = Quantity
  from inserted

  exec spRemoveProductStock @ProductId, @Quantity
end
