CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] nvarchar(255),
  [Name] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [StoreRestaurant] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Type] nvarchar(255) NOT NULL,
  [Name] nvarchar(255) NOT NULL,
  [Description] nvarchar(255),
  [CategoryId] int NOT NULL,
  [Location] nvarchar(255),
  [ContactInfo] nvarchar(255),
  [UserId] int NOT NULL
)
GO

CREATE TABLE [Category] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Tag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [StoreRestaurantTag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [StoreRestaurantId] int NOT NULL,
  [TagId] int NOT NULL
)
GO

ALTER TABLE [StoreRestaurant] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [StoreRestaurant] ADD FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id])
GO

ALTER TABLE [StoreRestaurantTag] ADD FOREIGN KEY ([TagId]) REFERENCES [Tag] ([Id])
GO

ALTER TABLE [StoreRestaurantTag] ADD FOREIGN KEY ([StoreRestaurantId]) REFERENCES [StoreRestaurant] ([Id]) ON DELETE CASCADE
GO
