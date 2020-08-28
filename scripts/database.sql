IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828165736_Initial')
BEGIN
    IF SCHEMA_ID(N'Application') IS NULL EXEC(N'CREATE SCHEMA [Application];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828165736_Initial')
BEGIN
    CREATE TABLE [Application].[Users] (
        [id] uniqueidentifier NOT NULL,
        [Email] nvarchar(150) NOT NULL,
        [Username] nvarchar(max) NULL,
        [Address_StreetName] nvarchar(150) NULL,
        [Address_StreetNumber] nvarchar(10) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828165736_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'Email', N'Username') AND [object_id] = OBJECT_ID(N'[Application].[Users]'))
        SET IDENTITY_INSERT [Application].[Users] ON;
    INSERT INTO [Application].[Users] ([id], [Email], [Username])
    VALUES ('be20ff2c-d1e6-47d4-8a2c-40cd0e528c6c', N'someuser@user.dk', N'someuser@user.dk');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'Email', N'Username') AND [object_id] = OBJECT_ID(N'[Application].[Users]'))
        SET IDENTITY_INSERT [Application].[Users] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828165736_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'Email', N'Username') AND [object_id] = OBJECT_ID(N'[Application].[Users]'))
        SET IDENTITY_INSERT [Application].[Users] ON;
    INSERT INTO [Application].[Users] ([id], [Email], [Username])
    VALUES ('7d0cc1c1-f97c-479e-9df2-560c8293e934', N'otheruser@user.dk', N'otheruser@user.dk');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'Email', N'Username') AND [object_id] = OBJECT_ID(N'[Application].[Users]'))
        SET IDENTITY_INSERT [Application].[Users] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828165736_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'Email', N'Username') AND [object_id] = OBJECT_ID(N'[Application].[Users]'))
        SET IDENTITY_INSERT [Application].[Users] ON;
    INSERT INTO [Application].[Users] ([id], [Email], [Username])
    VALUES ('e8f2bce5-c55f-4965-89f3-71439ffffe3a', N'lastuser@user.dk', N'lastuser@user.dk');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'Email', N'Username') AND [object_id] = OBJECT_ID(N'[Application].[Users]'))
        SET IDENTITY_INSERT [Application].[Users] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828165736_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Application].[Users] ([Email]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828165736_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200828165736_Initial', N'3.1.7');
END;

GO

