# Admete


## 現狀

某個 report 的 loading 太慢，需要優化。
看了一下程式碼，發現架構如下：

後端
* 有一個 endpoint `/api/Leekie/GameResults` 用來查詢遊戲結果
* 該 endpoint 沒有任何參數
* 當呼叫該 endpoint 時，會去資料庫撈取從 `2023-01-01` 到現在的所有遊戲結果，範例如下
    ```json
    [
        {
            "ProductType": 5,
            "gameID": 1,
            "stationName": "Sic Bo A",
            "startTime": "2023-01-01 02:03:04",
            "gameDetail": "123"
        },
        {
            "ProductType": 3,
            "gameID": 3,
            "stationName": "Roulette B",
            "startTime": "2024-01-01 03:04:05",
            "gameDetail": "225"
        }
    ]
    ```

前端
* 從後端撈到遊戲結果以後，只保留特定時間區間、特定遊戲產品的資料，並且按照時間、GameId排序

## Requirements

優化方向是改成後端只撈取需要的資料。

> The codes in folder `MockedDatabase` should not be modified.

- [ ] 輸入起迄時間、Product Type，回傳該時間區間內的特定遊戲的結果
- [ ] 遊戲結果需要依照時間排序，由舊到新
- [ ] 如果有兩局遊戲時間一樣，則依照 gameId 由小到大排序
- [ ] 僅能查詢到2023/1/1以後的遊戲結果，如果查詢的起始時間過早，回覆 Status Code 400，並加上錯誤代碼與訊息： `Start time must be later than 2023-01-01.`
- [ ] 如果起始時間超過結束時間，回覆 Status Code 400，並加上錯誤代碼與訊息： `End time must be greater than start time.`
- [ ] 如果成功查詢到遊戲結果，回覆 Status Code 200，並回傳遊戲結果
- [ ] 如果查詢時間區間內沒有遊戲結果，回覆 Status Code 200，並回傳空陣列
- [ ] 應該要有自動化測試
