use Sales

create procedure spUpdateProductStock
  @ProductId int,
  @Stock int
as
begin
  update Product
  set
    Stock = @Stock
  where
    Id = @ProductId
end
