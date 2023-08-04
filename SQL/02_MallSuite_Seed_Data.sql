USE [MallSuite];
GO

-- Clearing the tables
DELETE FROM [StoreRestaurantTag];
DBCC CHECKIDENT ('StoreRestaurantTag', RESEED, 0);
GO

DELETE FROM [StoreRestaurant];
DBCC CHECKIDENT ('StoreRestaurant', RESEED, 0);
GO

DELETE FROM [Tag];
DBCC CHECKIDENT ('Tag', RESEED, 0);
GO

DELETE FROM [Category];
DBCC CHECKIDENT ('Category', RESEED, 0);
GO

DELETE FROM [User];
DBCC CHECKIDENT ('User', RESEED, 0);
GO

-- add seed data
set identity_insert [User] on
INSERT INTO [User] ([Id], [FirebaseUserId], [Name], [Email])
VALUES (1, '2T7dN7HV9jdUmOcNvOkQe80jKLk1', 'Javier', 'javier@gmail.com'), 
       (2, '87NT8nu5wqRxbkuJbMfAufulodd2', 'Michael', 'michael@gmail.com');
set identity_insert [User] off
GO

set identity_insert [Category] on
INSERT INTO [Category] ([Id], [Name])
VALUES (1, 'Food'),
       (2, 'Clothing'),
       (3, 'Electronics'),
       (4, 'Books'),
       (5, 'Toys'),
	   (6, 'Home'),
	   (7, 'Health & Beauty');
set identity_insert [Category] off
GO

set identity_insert [Tag] on
INSERT INTO [Tag] ([Id], [Name])
VALUES (1, 'High End'),
       (2, 'Budget Friendly'),
       (3, 'Lifestyle'),
       (4, 'Entertainment'),
       (5, 'Family Friendly'),
       (6, 'Kids'),
       (7, 'Adults');
set identity_insert [Tag] off
GO

set identity_insert [StoreRestaurant] on
INSERT INTO [StoreRestaurant] ([Id], [Type], [Name], [Description], [CategoryId], [Location], [ContactInfo], [UserId])
VALUES (1, 'Store', 'Book Store', 'A wide range of books.', 4, 'Level 1', '1234567890', 1),
       (2, 'Restaurant', 'Italian Bistro', 'Authentic Italian Cuisine.', 1, 'Level 2', '0987654321', 2),
       (3, 'Store', 'Clothing Store', 'A wide range of clothing.', 2, 'Level 1', '1234567784', 1),
	   (4, 'Restaurant', 'Chinese Restaurant', 'Authentic Chinese Cuisine.', 1, 'Level 2', '0987654852', 2);
set identity_insert [StoreRestaurant] off
GO

set identity_insert [StoreRestaurantTag] on
INSERT INTO [StoreRestaurantTag] ([Id], [StoreRestaurantId], [TagId])
VALUES (1, 1, 1),
       (2, 1, 3),
       (3, 2, 2),
       (4, 2, 4),
       (5, 3, 2),
	   (6, 3, 3);
set identity_insert [StoreRestaurantTag] off
GO
