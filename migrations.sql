IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Article] (
    [ArticleId] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY ([ArticleId])
);
GO

CREATE TABLE [Tag] (
    [TagId] nvarchar(20) NOT NULL,
    [Content] ntext NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([TagId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211121065741_V0', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Article].[Title]', N'Name', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211121074103_V1', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Tag] DROP CONSTRAINT [PK_Tag];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tag]') AND [c].[name] = N'TagId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Tag] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Tag] DROP COLUMN [TagId];
GO

ALTER TABLE [Tag] ADD [NewTagId] int NOT NULL IDENTITY;
GO

ALTER TABLE [Tag] ADD CONSTRAINT [PK_Tag] PRIMARY KEY ([NewTagId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211121105011_V2-RemoveTagId', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Tag].[NewTagId]', N'TagId', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211121105224_V2-RenameTagId', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ArticleTags] (
    [ArticleTagId] int NOT NULL IDENTITY,
    [TagId] int NOT NULL,
    [ArticleId] int NOT NULL,
    CONSTRAINT [PK_ArticleTags] PRIMARY KEY ([ArticleTagId]),
    CONSTRAINT [FK_ArticleTags_Article_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [Article] ([ArticleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ArticleTags_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tag] ([TagId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ArticleTags_ArticleId] ON [ArticleTags] ([ArticleId]);
GO

CREATE UNIQUE INDEX [IX_ArticleTags_TagId_ArticleId] ON [ArticleTags] ([TagId], [ArticleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211121112109_V3', N'5.0.0');
GO

COMMIT;
GO

