-- Полная стоимость заказа (покупателя) с учётом стоимости материалов на производство продукции
SELECT 
    o.OrderID,
    c.ClientName AS CustomerName,
    o.OrderDate,
    o.Status,
    p.ProductName,
    oi.ProductQuantity AS OrderedQuantity,
    
    (
        SELECT SUM(m.Price * pm.MaterialQuantity)
        FROM ProductionProducts pp
        INNER JOIN ProductionMaterials pm ON pp.ProductionMaterialKey = pm.ProductionMaterialID
        INNER JOIN Materials m ON pm.MaterialKey = m.MaterialID
        WHERE pp.ProductKey = p.ProductID
    ) AS MaterialCostPerProductUnit,
    
    (
        SELECT SUM(m.Price * pm.MaterialQuantity) * oi.ProductQuantity
        FROM ProductionProducts pp
        INNER JOIN ProductionMaterials pm ON pp.ProductionMaterialKey = pm.ProductionMaterialID
        INNER JOIN Materials m ON pm.MaterialKey = m.MaterialID
        WHERE pp.ProductKey = p.ProductID
    ) AS TotalMaterialCostForOrder,
    
    oi.Summ AS OrderSumFromItems,
    -
    oi.Summ AS FinalOrderCost  
FROM Orders o
INNER JOIN Clients c ON o.ClientKey = c.ClientID
INNER JOIN OrderItems oi ON o.OrderItemKey = oi.OrderItemID
INNER JOIN Products p ON oi.ProductKey = p.ProductID
WHERE c.ClientID = 1  