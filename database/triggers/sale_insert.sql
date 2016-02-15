use Sales

create trigger tSaleInsert
on Sale
for insert
as
begin
  declare
    @ClientId int,
    @DateSale datetime

  select @ClientId = ClientId, @DateSale = DateSale
  from inserted

  exec spUpdateVIP @ClientId, @DateSale
end
