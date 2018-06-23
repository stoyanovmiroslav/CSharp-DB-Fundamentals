WITH CTE_Countries_MaxElevation_MountainId(CountryName,MaxElevation,MountainId)
     AS (
     SELECT c.CountryName,
            MAX(p.Elevation) AS [MaxElevation],
            mc.MountainId
     FROM Countries AS c
          LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
          LEFT JOIN Peaks AS p ON p.MountainId = mc.MountainId
          LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
     GROUP BY c.CountryName, mc.MountainId),

     CTE_Countries_MaxElevation(Country, MaxElevation)
     AS (
     SELECT cmm.CountryName AS Country,
            MAX(cmm.MaxElevation) AS MaxElevation
     FROM CTE_Countries_MaxElevation_MountainId AS cmm
     GROUP BY cmm.CountryName)

     SELECT TOP (5) cm.Country AS [Country],
                    ISNULL(p.PeakName, '(no highest peak)') AS [Highest Peak Name],
                    ISNULL(cm.MaxElevation, '0') AS [Highest Peak Elevation],
                    ISNULL(m.MountainRange, '(no mountain)') AS [Mountain]
     FROM CTE_Countries_MaxElevation AS cm
          LEFT JOIN CTE_Countries_MaxElevation_MountainId AS cmm ON cmm.CountryName = cm.Country
                                                                    AND cmm.MaxElevation = cm.MaxElevation
          LEFT JOIN Peaks AS p ON p.Elevation = cm.MaxElevation
                                  AND cmm.CountryName = cm.Country
          LEFT JOIN Mountains AS m ON m.Id = p.MountainId
     ORDER BY cm.Country, p.PeakName;