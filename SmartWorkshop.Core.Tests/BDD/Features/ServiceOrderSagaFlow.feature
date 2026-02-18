# language: pt-BR

Funcionalidade: Gerenciamento Completo de Ordem de Serviço
  Como um cliente da oficina mecânica
  Eu quero abrir uma ordem de serviço e acompanhar todo o fluxo
  Para que meu veículo seja reparado de forma transparente

  Contexto:
    Dado que existe um cliente cadastrado com CPF "123.456.789-01"
    E que existe um veículo cadastrado com placa "ABC-1234"
    E que o sistema está operacional

  Cenário: Fluxo completo de ordem de serviço bem-sucedida
    Dado que o cliente solicita abertura de ordem de serviço para o veículo
    Quando a ordem de serviço é criada com a descrição "Troca de óleo e filtros"
    Então a ordem de serviço deve ser criada com status "New"
    E um evento "ServiceOrderCreatedIntegrationEvent" deve ser publicado

    Quando o Billing Service recebe o evento de criação de OS
    Então um orçamento deve ser criado automaticamente
    E o orçamento deve conter os serviços solicitados
    E o orçamento deve ser enviado ao cliente por email
    E um evento "QuoteCreatedIntegrationEvent" deve ser publicado

    Quando o cliente aprova o orçamento
    Então o status do orçamento deve mudar para "Approved"
    E um evento "QuoteApprovedIntegrationEvent" deve ser publicado

    Quando o Production Service recebe o evento de aprovação
    Então um item de trabalho deve ser criado na fila
    E o status do item deve ser "Pending"
    E o status da OS deve mudar para "InProgress"

    Quando um técnico inicia o diagnóstico
    Então o status do item deve mudar para "InDiagnosis"
    E logs de diagnóstico devem ser registrados no MongoDB

    Quando o diagnóstico é concluído
    Então o status deve mudar para "DiagnosisCompleted"
    E um evento "DiagnosticCompletedEvent" deve ser publicado

    Quando o técnico inicia os reparos
    Então o status deve mudar para "InRepair"
    E logs de reparo devem ser registrados no MongoDB

    Quando os reparos são concluídos
    Então o status deve mudar para "RepairCompleted"
    E a verificação de qualidade deve ser iniciada

    Quando a verificação de qualidade é aprovada
    Então o status deve mudar para "Completed"
    E um evento "WorkCompletedIntegrationEvent" deve ser publicado

    Quando o Billing Service recebe o evento de trabalho concluído
    Então uma fatura deve ser gerada
    E a fatura deve conter todos os itens do orçamento
    E a fatura deve ser enviada ao cliente
    E um evento "InvoiceIssuedIntegrationEvent" deve ser publicado

    Quando o cliente efetua o pagamento via Mercado Pago
    Então o pagamento deve ser processado
    E o status do pagamento deve ser "Approved"
    E um evento "PaymentConfirmedIntegrationEvent" deve ser publicado

    Quando o OS Service recebe o evento de pagamento confirmado
    Então o status da ordem de serviço deve mudar para "Completed"
    E o cliente deve ser notificado da conclusão

  Cenário: Rollback quando orçamento é rejeitado
    Dado que existe uma ordem de serviço com status "New"
    E que um orçamento foi criado
    Quando o cliente rejeita o orçamento com motivo "Valor muito alto"
    Então o status do orçamento deve mudar para "Rejected"
    E um evento "QuoteRejectedIntegrationEvent" deve ser publicado
    E o status da ordem de serviço deve mudar para "QuoteRejected"
    E nenhum item de trabalho deve ser criado na produção

  Cenário: Compensação quando pagamento falha
    Dado que uma ordem de serviço está concluída
    E que uma fatura foi emitida
    Quando o pagamento falha com erro "Cartão recusado"
    Então um evento "PaymentFailedIntegrationEvent" deve ser publicado
    E o status da fatura deve ser "Overdue"
    E o cliente deve ser notificado para tentar novamente
    E a ordem de serviço deve permanecer com status "Pending"
