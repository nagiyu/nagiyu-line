# 構成

## システム構成

![](./assets/system-structure.drawio.svg)

- LINE Messaging API とは Webhook イベントの受け取り、返信の送信を行う

- OpenAI API とは AI による会話の生成を行う

- AWS DynamoDB とはトーク履歴の取得、トークの保存を行う
