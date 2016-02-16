use Sales

create procedure spSelectSaleByDate
  @StartDate datetime,
  @EndDate datetime
as
begin
  select Id, ClientId, DateSale, Total, Discount, FinalTotal
  from Sale
  where
    DateSale >= @StartDate
    and DateSale <= @EndDate
end
