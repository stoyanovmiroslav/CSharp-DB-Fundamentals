SELECT mc.CountryCode,
       m.MountainRange,
       p.PeakName,
       P.Elevation
FROM Peaks AS p
     JOIN Mountains AS m ON m.Id = p.MountainId
     JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
WHERE MC.CountryCode = 'BG'
      AND p.Elevation > 2835
ORDER BY p.Elevation DESC;