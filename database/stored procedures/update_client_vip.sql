use Sales

create procedure spUpdateClientVIP
  @ClientId int,
  @DateSale datetime
as
begin
  declare
    @VIP bit,
    @CountSales int,
    @DateCompare datetime,
    @DateLastSale datetime

  select @VIP = VIP from Client where Id = @ClientId
  set @DateCompare = dateadd(month, -12, @DateSale)
  select @CountSales = count(Id) from Sale
    where ClientId = @ClientId
      and DateSale between @DateCompare and @DateSale
  select top(1) @DateLastSale = DateSale from Sale
    where ClientId = @ClientId
      and DateSale < @DateSale
    order by DateSale desc

  if (@CountSales >= 20)
    set @VIP = 1

  if (datediff(day, @DateLastSale, @DateSale) > 90)
    set @VIP = 0

  update Client
  set
    VIP = @VIP
  where Id = @ClientId
end
