CREATE PROCEDURE [dbo].[SPInventory_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	FROM Inventory

END
