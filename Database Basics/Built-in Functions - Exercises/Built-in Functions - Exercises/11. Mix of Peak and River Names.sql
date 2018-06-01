SELECT PeakName , RiverName, LOWER(PeakName + SUBSTRING(RiverName, 2, LEN(RiverName) - 1)) AS Mix
FROM Rivers, Peaks
WHERE RIGHT(PeakName, 1) = LEFT(RiverName, 1)
ORDER BY Mix