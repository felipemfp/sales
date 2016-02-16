use Sales

create procedure spAddProductStock
  @ProductId int,
  @Quantity int
as
begin
  update Product
  set
    Stock = Stock + @Quantity
  where
    Id = @ProductId
end
