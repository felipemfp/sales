use Sales
go
declare
	@ClientId int = ABS(Checksum(NewID()) % 6) + 1,
	@DateSale datetime,
	@SaleId int,
	@Itens int,
	@Count int = -25,
	@ProductId int,
	@Quantity int
while (@Count <= 0)
begin
	set @DateSale = dateadd(month, @Count, getdate())
	exec spInsertSale @ClientId, @DateSale, @SaleId OUTPUT
	set @Itens = ABS(Checksum(NewID()) % 4) + 1
	while (@Itens > 0)
	begin
		set @ProductId = ABS(Checksum(NewID()) % 15) + 1
		set @Quantity = ABS(Checksum(NewID()) % 9) + 1
		exec spInsertSaleProduct @SaleId, @ProductId, @Quantity
		set @Itens = @Itens - 1
	end
	set @Count = @Count + 1
end
