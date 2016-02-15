use Sales

create procedure spInsertSale
  @ClientId int,
  @DateSale datetime = getdate(),
  @SaleId int OUTPUT
as
begin
  insert into Sale(ClientId, DateSale)
  values (@ClientId, @DateSale)

  set @SaleId = SCOPE_IDENTITY()
end
