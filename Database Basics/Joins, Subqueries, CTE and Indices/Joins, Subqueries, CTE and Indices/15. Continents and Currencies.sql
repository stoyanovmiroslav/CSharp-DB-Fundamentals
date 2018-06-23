WITH Countries_CTE (ContinentCode, CurrencyCode, CurrencyUsage)
AS
(
SELECT c.ContinentCode, 
       c.CurrencyCode,
	   COUNT(c.CurrencyCode) AS CurrencyUsage      
FROM Countries AS c
GROUP BY CurrencyCode, ContinentCode
HAVING  COUNT(c.CurrencyCode) > 1
)

SELECT c.ContinentCode,
       c.CurrencyCode,
       c.CurrencyUsage
FROM
( 
SELECT ContinentCode, 
       MAX(CurrencyUsage) AS MaxCurrencyUsage
  FROM Countries_CTE
  GROUP BY ContinentCode
 )AS Country_MaxCurrencyUsage
 JOIN Countries_CTE AS c
 ON c.CurrencyUsage  = Country_MaxCurrencyUsage.MaxCurrencyUsage
 AND c.ContinentCode = Country_MaxCurrencyUsage.ContinentCode 