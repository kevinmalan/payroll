CREATE TABLE [dbo].[FlatRate] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [From]             DECIMAL (18, 10) NOT NULL,
    [To]               DECIMAL (18, 10) NOT NULL,
    [TaxPercentage]    DECIMAL (18, 10) NOT NULL,
    [AdditionalAmount] DECIMAL (18, 10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

