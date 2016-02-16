use Sales

declare
	@ClientId int = 1,
	@DateSale datetime,
	@SaleId int,
	@Count int = 0
while (@Count <= 0)
begin
	set @DateSale = dateadd(month, @Count, getdate())
	exec spInsertSale @ClientId, @DateSale, @SaleId OUTPUT

	exec spInsertSaleProduct @SaleId, 10, 5
	exec spInsertSaleProduct @SaleId, 11, 5

	set @Count = @Count + 1
end
