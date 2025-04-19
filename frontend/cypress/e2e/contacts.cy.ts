describe('Gerenciamento de contatos', () => {
  it('deve navegar da home para a página de criação e criar um contato', () => {
    // 1. Visita a home
    cy.visit('/')

    // 2. Clica no botão de adicionar contato
    cy.get('#add-contact').click()

    // 3. Verifica que foi redirecionado para /contato
    cy.location('pathname').should('eq', '/contato')

    // 4. Preenche o formulário
    cy.get('input[name="name"]').type('Teste Usuário')
    cy.get('input[name="phoneNumber"]').type('(11) 91234-5678')
    cy.get('input[name="email"]').type('teste@exemplo.com')

    // 5. Envia
    cy.get('button[type="submit"]').click()

    // 3. Verifica que foi redirecionado para /
    cy.location('pathname').should('include', '/contato')

    // 6. Verifica toast de sucesso
    cy.contains('Contato criado!').should('be.visible')
  })
})
