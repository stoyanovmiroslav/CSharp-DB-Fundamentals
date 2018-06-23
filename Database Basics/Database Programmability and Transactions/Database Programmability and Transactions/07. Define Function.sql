CREATE FUNCTION ufn_IsWordComprised(@SetOfLetters NVARCHAR(MAX), @Word NVARCHAR(MAX)) 
RETURNS BIT
AS 
BEGIN 
       DECLARE @i INT = 1
       DECLARE @IsComprised BIT = 1
       
       WHILE @i <= LEN(@Word)
       BEGIN
		    DECLARE @currentLetter CHAR = SUBSTRING(@word, @i, 1);
			IF(CHARINDEX(@currentLetter, @SetOfLetters, 1 ) <= 0)
			BEGIN 
			SET @IsComprised = 0
			END
			SET @i = @i + 1
       END
	   RETURN @IsComprised
END
GO

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves')
