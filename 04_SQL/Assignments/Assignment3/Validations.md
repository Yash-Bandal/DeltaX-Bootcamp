### SP With Validation
```sql
USE IMDB_Dummy;
GO

CREATE OR ALTER PROCEDURE Foundation.usp_InsertMovie
(
    @Name VARCHAR(150),
    @YearOfRelease INT,
    @Plot VARCHAR(300),
    @PosterImagePath VARCHAR(200),
    @ProducerId INT,
    @Language VARCHAR(50),
    @Profit INT,
    @ActorIds VARCHAR(50)
)
AS
BEGIN

    -- =========================================
    -- VALIDATIONS
    -- =========================================

    -- Movie name
    IF @Name IS NULL OR LTRIM(RTRIM(@Name)) = ''
    BEGIN
        THROW 50001, 'Movie name cannot be NULL or empty.', 1;
    END;


    -- Year
    IF @YearOfRelease IS NULL
    BEGIN
        THROW 50002, 'Year of release is required.', 1;
    END;

    IF @YearOfRelease < 1888 OR @YearOfRelease > YEAR(GETDATE())
    BEGIN
        THROW 50003, 'Invalid year of release.', 1;
    END;


    -- Producer
    IF @ProducerId IS NULL
    BEGIN
        THROW 50004, 'ProducerId is required.', 1;
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM Foundation.Producers
        WHERE Id = @ProducerId
    )
    BEGIN
        THROW 50005, 'Producer does not exist.', 1;
    END;


    -- Language
    IF @Language IS NULL OR LTRIM(RTRIM(@Language)) = ''
    BEGIN
        THROW 50006, 'Language cannot be NULL or empty.', 1;
    END;


    -- Actors
    IF @ActorIds IS NULL OR LTRIM(RTRIM(@ActorIds)) = ''
    BEGIN
        THROW 50007, 'At least one ActorId is required.', 1;
    END;


    -- Check ActorId format
    IF EXISTS
    (
        SELECT 1
        FROM STRING_SPLIT(@ActorIds, ',') S
        WHERE TRY_CAST(LTRIM(RTRIM(S.value)) AS INT) IS NULL
    )
    BEGIN
        THROW 50008, 'ActorIds must contain only integer IDs.', 1;
    END;


    -- Check ActorId existence
    IF EXISTS
    (
        SELECT 1
        FROM STRING_SPLIT(@ActorIds, ',') S
        LEFT JOIN Foundation.Actors A
            ON A.Id = TRY_CAST(LTRIM(RTRIM(S.value)) AS INT)
        WHERE A.Id IS NULL
    )
    BEGIN
        THROW 50009, 'One or more ActorIds do not exist.', 1;
    END;


    -- =========================================
    -- DEFAULT VALUES USING ISNULL()
    -- =========================================

    SET @Plot = ISNULL(@Plot, 'No plot available.');

    SET @PosterImagePath = ISNULL(@PosterImagePath, 'default-poster.jpg');

    SET @Profit = ISNULL(@Profit, 0);


    -- =========================================
    -- INSERT
    -- =========================================

    BEGIN TRY

        BEGIN TRANSACTION;

        DECLARE @MovieId INT;

        -- Insert Movie
        INSERT INTO Foundation.Movies
        (
            Name,
            YearOfRelease,
            Plot,
            PosterImagePath,
            ProducerId,
            Language,
            Profit
        )
        VALUES
        (
            @Name,
            @YearOfRelease,
            @Plot,
            @PosterImagePath,
            @ProducerId,
            @Language,
            @Profit
        );


        -- Get newly inserted MovieId
        SET @MovieId = SCOPE_IDENTITY();


        -- Insert Actors
        INSERT INTO Foundation.Actor_Movies
        (
            MovieId,
            ActorId
        )
        SELECT
            @MovieId,
            TRY_CAST(LTRIM(RTRIM(value)) AS INT)
        FROM STRING_SPLIT(@ActorIds, ',');


        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH;

END;
GO
```
