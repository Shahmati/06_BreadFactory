SELECT 
    o.OrderID,
    c.ClientName,
    p.ProductName,
    oi.ProductQuantity,
    oi.Summ AS OrderAmount,
    ROUND(
        (
            SELECT SUM(m.Price * pm.MaterialQuantity)
            FROM ProductionMaterials pm
            INNER JOIN Materials m ON pm.MaterialKey = m.MaterialID
            WHERE pm.ProductionMaterialID = 1  -- нужно знать ID состава
        ) * oi.ProductQuantity, 2
    ) AS MaterialCost
FROM Orders o
INNER JOIN Clients c ON o.ClientKey = c.ClientID
INNER JOIN OrderItems oi ON o.OrderItemKey = oi.OrderItemID
INNER JOIN Products p ON oi.ProductKey = p.ProductID;