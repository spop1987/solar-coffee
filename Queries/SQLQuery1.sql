use SolarCoffeeDB;

INSERT INTO Products ("CreatedOn", "UpdatedOn", "Name", "Description", "Price", "IsTaxable", "IsArchived") 
	VALUES
	(GETDATE(), GETDATE(),  '10 LB Dark Roast', '10 LB bag of dark roast coffee beans', 100, 1, 0),
	(GETDATE(), GETDATE(),  '30 LB Dark Roast', '30 LB bag of dark roast coffee beans', 280, 1, 0),
	(GETDATE(), GETDATE(),  '50 LB Dark Roast', '50 LB bag of dark roast coffee beans', 450, 1, 0),
	(GETDATE(), GETDATE(),  '10 LB Light Roast', '10 LB bag of Light roast coffee beans', 100, 1, 0),
	(GETDATE(), GETDATE(),  '30 LB Light Roast', '30 LB bag of Light roast coffee beans', 280, 1, 0),
	(GETDATE(), GETDATE(),  '30 LB Light Roast', '30 LB bag of Light roast coffee beans', 450, 1, 0);


SELECT * FROM Products
