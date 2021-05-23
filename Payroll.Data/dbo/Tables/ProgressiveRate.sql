CREATE TABLE [dbo].[ProgressiveRate] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [From]             DECIMAL (19, 4) NOT NULL,
    [To]               DECIMAL (19, 4) NOT NULL,
    [TaxPercentage]    DECIMAL (9, 4) NOT NULL,
    [AdditionalAmount] DECIMAL (19, 4) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

