use Sales

create procedure spRemoveProductStock
  @ProductId int,
  @Quantity int
as
begin
  declare
    @Stock int

  select @Stock = Stock from Product where Id = @ProductId
  if (@Stock >= @Quantity)
  begin
    update Product
    set
      Stock = @Stock - @Quantity
    where
      Id = @ProductId
  end
  else
  begin
    raiserror('Product stock has been exceeded | Product %i', 16, 1, @ProductId)
    rollback transaction
    return
  end
end
