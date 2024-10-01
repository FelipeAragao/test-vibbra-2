# Sistema de Notificações

API de gerenciamento de aplicativos e envio de notificações.
Desenvolvida para o teste técnico da Vibbra.

## Rotas e payloads

### Usuário
- Login

```
POST 
Rota: /login

Request:
{
  "login": "user@example.com",
  "password": "string"
}

Response:
{
	"token": string,
	"user": {
        "id": string,
        "name": string,
        "email": "user@example.com",
  	 }
}

```
- Registrar usuário
```
POST
Rota: /users/register

Request:
{
  "email": "user@example.com",
  "password": "string",
  "name": "string",
  "company_name": "string",
  "phone_number": "string",
  "company_address": "string"
}

Response
{
   "id": guid 
}
```

- Obter usuário
    - ID: GUID
```
GET
Rota: /users/{id}

Response
{
    "email": string,
	"name": string,
	"company_name": string,
	"phone_number": string,
	"company_address": string
}
```
## [Requer Autorização]
### Apps
- Obter todos
```
GET
Rota: /apps

Request
{
  "app_name": "string"
}

Response
{
    [
        {
            "app_name": string
            "app_token": string
        }
    ]
}
```
- Obter app
```
GET
Rota: /apps/{id}

Response
{
  "app_id": int,
  "app_name": "string",
  "app_token": "string",
  "active_channels": {
    "web_push": bool,
    "email": bool,
    "sms": bool
  }
}
```

- Criar app
```
POST
Rota: /apps

Request
{
    "app_name": string
}
Response (código 201)
{
  "app_id": int,
  "app_token": "string",
}
```

### Canal WebPush
- Criar configuração
```
POST
Rota: /apps/{appId}/webpushes/settings

Request
{
  "site": {
    "name": "string",
    "address": "string",
    "url_icon": "string"
  },
  "allow_notification": {
    "message_text": "string",
    "allow_button_text": "string",
    "deny_button_text": "string"
  },
  "welcome_notification": {
    "message_title": "string",
    "message_text": "string",
    "enable_url_redirect": bool,
    "url_redirect": "string"
  }
}

Response (código 201)
```
- Obter configuração
```
GET
Rota: /apps/{appId}/webpushes/settings

Response
{
  "site": {
    "name": "string",
    "address": "string",
    "url_icon": "string"
  },
  "allow_notification": {
    "message_text": "string",
    "allow_button_text": "string",
    "deny_button_text": "string"
  },
  "welcome_notification": {
    "message_title": "string",
    "message_text": "string",
    "enable_url_redirect": bool,
    "url_redirect": "string"
  }
}
```
- Ativar/Desativar canal
```
PUT
Rota: /apps/{appId}/webpushes/settings

Response
{
    "previous_status": int,
    "current_status": int
}
```

- Criar notificação
```
POST
Rota: /apps/{appId}/webpushes/notification

Request
{
  "audience_segments": [
    "string"
  ],
  "message_title": "string",
  "message_text": "string",
  "icon_url": "string",
  "redirect_url": "string"
}

Response
{
    "notification_id": int
}
```
- Obter notificação por data
```
GET
Rota: /apps/{appId}/webpushes/notification?initdate={initdate}&enddate={enddate}

Response
{
    [
        {
            “notification_id”: int,
            “send_date”: date,
        }
    ]
}
```
- Obter detalhes de notificação
```
GET
Rota: /apps/{appId}/webpushes/notification/{notificationId}

Response
{
	“notification_id”: integer,
	“audience_segments”: [
        "string”
	],
	“message_title”: string,
	“message_text”: string,
	“icon_url”: string,
	“redirect_url”: string,
	“send_date”: date,
	“received”: bool,
	“opened”: bool,
    "clicked": bool
}

```
### Canal Email
- Criar configuração
```
POST
Rota: /apps/{appId}/emails/settings

Request
{
  "server": {
    "smtp_name": "string",
    "smtp_port": "string",
    "user_login": "string",
    "user_password": "string"
  },
  "sender": {
    "name": "string",
    "email": "string"
  },
  "email_templates": [
    {
      "name": "string",
      "uri": "string"
    }
  ]
}

Response (código 201)
```
- Obter configuração
```
GET
Rota: /apps/{appId}/webpushes/settings

Response
{
  "server": {
    "smtp_name": "string",
    "smtp_port": "string",
    "user_login": "string",
    "user_password": "string"
  },
  "sender": {
    "name": "string",
    "email": "string"
  },
  "email_templates": [
    {
      "name": "string",
      "uri": "string"
    }
  ]
}
```
- Ativar/Desativar canal
```
PUT
Rota: /apps/{appId}/webpushes/settings

Response
{
    "previous_status": int,
    "current_status": int
}
```

- Criar notificação
```
POST
Rota: /apps/{appId}/webpushes/notification

Request
{
  "receiver_email": [
    "string"
  ],
  "email_template_name": "string",
  "message_text": "string",
  "received": true,
  "opened": true,
  "clicked": true
}


Response
{
    "notification_id": int
}
```
- Obter notificação por data
```
GET
Rota: /apps/{appId}/webpushes/notification?initdate={initdate}&enddate={enddate}

Response
{
    [
        {
            “notification_id”: int,
            “send_date”: date,
        }
    ]
}
```
- Obter detalhes de notificação
```
GET
Rota: /apps/{appId}/webpushes/notification/{notificationId}

Response
{
	“notification_id”: integer,
    "send_date": date,
	"receiver_email": [
        "string"
    ],
    "email_template_name": "string",
    "message_text": "string",
    "received": true,
    "opened": true,
    "clicked": true
}

```
### Canal SMS
- Criar configuração
```
POST
Rota: /apps/{appId}/webpushes/settings

Request
{
  "sms_provider": {
    "name": "string",
    "login": "string",
    "password": "string"
  }
}

Response (código 201)
```
- Obter configuração
```
GET
Rota: /apps/{appId}/webpushes/settings

Response
{
  "sms_provider": {
    "name": "string",
    "login": "string",
    "password": "string"
  }
}
```
- Ativar/Desativar canal
```
PUT
Rota: /apps/{appId}/webpushes/settings

Response
{
    "previous_status": int,
    "current_status": int
}
```

- Criar notificação
```
POST
Rota: /apps/{appId}/webpushes/notification

Request
{
  "phone_number": [
    "string"
  ],
  "message_text": "string"
}

Response
{
    "notification_id": int
}
```
- Obter notificação por data
```
GET
Rota: /apps/{appId}/webpushes/notification?initdate={initdate}&enddate={enddate}

Response
{
    [
        {
            “notification_id”: int,
            “send_date”: date,
        }
    ]
}
```
- Obter detalhes de notificação
```
GET
Rota: /apps/{appId}/webpushes/notification/{notificationId}

Response
{
	“notification_id”: integer,
	“send_date”: date,
	"phone_number": [
        "string"
    ],
    "message_text": "string"
}

```

## Pendências

- Controlar níveis de permissão por usuário
- Exportar os dados em PDF/Excel
- Permitir autenticação via Google
- Habilitar autenticação automática
