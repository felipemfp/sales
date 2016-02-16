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
      @NewQuantity int,
      @Stock int

    select @NewProductId = ProductId, @NewQuantity = Quantity
    from inserted

    select @OldProductId = ProductId, @OldQuantity = Quantity
    from deleted

    select @Stock = Stock
    from Product
    where Id = @OldProductId

    set @Stock = @Stock + @OldQuantity
    exec spUpdateProductStock @ProductId, @Stock

    select @Stock = Stock
    from Product
    where Id = @NewProductId

    if (@Stock >= @NewQuantity)
      set @Stock = @Stock - @NewQuantity

      exec spUpdateProductStock @ProductId, @Stock
    else
      raiserror('Product stock has been exceeded | Product %i', 16, 1, @NewProductId)
      rollback transaction
      return
end
