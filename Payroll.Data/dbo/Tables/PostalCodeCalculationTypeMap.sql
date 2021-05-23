CREATE TABLE [dbo].[PostalCodeCalculationTypeMap] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [PostalCode]      NVARCHAR (50)    NOT NULL,
    [CalculationType] NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

