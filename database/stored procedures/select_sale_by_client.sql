use Sales

create procedure spSelectSaleByClient
  @ClientId int
as
begin
  select Id, ClientId, DateSale, Total, Discount, FinalTotal
  from Sale
  where ClientId = @ClientId
end
