use Sales

create trigger tSaleProductUpdate
on SaleProduct
for update
as
begin
  if update(Quantity) or update(ProductId)
    declare
      @OldProductId int,
      @OldQuantity int,
      @NewProductId int,
      @NewQuantity int

    select @NewProductId = ProductId, @NewQuantity = Quantity
    from inserted

    select @OldProductId = ProductId, @OldQuantity = Quantity
    from deleted

    exec spAddProductStock @OldProductId, @OldQuantity
    exec spRemoveProductStock @NewProductId, @NewQuantity
end
