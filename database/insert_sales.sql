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

	exec spInsertSaleProduct @SaleId, ABS(Checksum(NewID()) % 15) + 1, ABS(Checksum(NewID()) % 9) + 1
	exec spInsertSaleProduct @SaleId, ABS(Checksum(NewID()) % 15) + 1, ABS(Checksum(NewID()) % 9) + 1
	exec spInsertSaleProduct @SaleId, ABS(Checksum(NewID()) % 15) + 1, ABS(Checksum(NewID()) % 9) + 1
	exec spInsertSaleProduct @SaleId, ABS(Checksum(NewID()) % 15) + 1, ABS(Checksum(NewID()) % 9) + 1

	set @Count = @Count + 1
end
