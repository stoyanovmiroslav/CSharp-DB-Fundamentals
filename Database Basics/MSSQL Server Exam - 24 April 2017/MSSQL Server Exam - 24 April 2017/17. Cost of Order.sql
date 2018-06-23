CREATE FUNCTION udf_GetCost(@JobsId INT)
RETURNS DECIMAL(15,2)
AS 
BEGIN
 DECLARE @TotalAmound DECIMAL(15, 2)=
(
    SELECT ISNULL(SUM(p.Price * op.Quantity), 0) AS Total
    FROM Jobs AS j
         LEFT JOIN Orders AS o ON o.JobId = j.JobId
         LEFT JOIN OrderParts AS op ON op.OrderId = o.OrderId
         LEFT JOIN Parts AS p ON p.PartId = op.PartId
    WHERE j.JobId = @JobsId
    GROUP BY j.JobId
);
		
     IF(@TotalAmound IS NULL)
         BEGIN
             RETURN 0;
         END;
     RETURN @TotalAmound;
END
GO

SELECT dbo.udf_GetCost(123)