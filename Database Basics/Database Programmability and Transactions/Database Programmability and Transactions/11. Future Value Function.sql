CREATE FUNCTION ufn_CalculateFutureValue(@initialSum DECIMAL(15, 2), @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS DECIMAL(15, 4)
BEGIN 
   RETURN @initialSum * POWER(1 + @yearlyInterestRate, @numberOfYears)
END
GO

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)