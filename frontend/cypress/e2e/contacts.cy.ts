describe('Gerenciamento de contatos', () => {
  const randomName = `Usuário de teste ${Math.random().toString(36).substring(2, 15)}`
  const randomEditedName = `Usuário de teste editado ${Math.random().toString(36).substring(2, 15)}`

  it('deve navegar da home para a página de criação e criar um contato', () => {
    cy.visit('/')

    cy.get('#add-contact').click()

    cy.location('pathname').should('eq', '/contato')

    cy.get('input[name="name"]').type(randomName)
    cy.get('input[name="phoneNumber"]').type('(11) 91234-5678')
    cy.get('input[name="email"]').type('teste@exemplo.com')

    cy.get('button[type="submit"]').click()

    cy.location('pathname').should('include', '/contato')

    cy.contains('Contato criado!').should('be.visible')
  })

  it('deve filtrar pelo nome e exibir somente aquele contato', () => {
    cy.visit('/')

    cy.get('#search-input').should('be.visible')
    cy.get('#search-input').clear()
    cy.get('#search-input').type(randomName)

    cy.get('.p-panel', { timeout: 10000 }).should('have.length', 1)

    cy.get('.p-panel-header').first().should('contain.text', randomName)
  })

  it('deve editar um contato', () => {
    cy.visit('/')

    cy.contains('.p-panel-header', randomName).should('exist')

    cy.contains('.p-panel-header', randomName).click()

    cy.contains('.p-panel-header', randomName).parents('.p-panel').as('targetPanel')

    cy.get('@targetPanel').find('#edit-contact-btn').click()

    cy.location('pathname').should('match', /^\/contato\/[0-9a-f-]+$/)

    cy.get('input[name="name"]').should('be.visible')
    cy.get('input[name="name"]').clear()
    cy.get('input[name="name"]').type(randomEditedName)

    cy.get('input[name="phoneNumber"]').clear()
    cy.get('input[name="phoneNumber"]').type('(11) 92345-6789')

    cy.get('input[name="email"]').clear()
    cy.get('input[name="email"]').type('editado@exemplo.com')

    cy.get('button[type="submit"]').click()

    cy.get('.p-toast-message .p-toast-summary')
      .should('be.visible')
      .and('contain', 'Contato atualizado!')
  })

  it('deve excluir um contato', () => {
    cy.visit('/')

    cy.contains('.p-panel-header', randomEditedName).should('exist')

    cy.contains('.p-panel-header', randomEditedName).click()

    cy.contains('.p-panel-header', randomEditedName).parents('.p-panel').as('targetPanel')

    cy.get('@targetPanel').find('#delete-contact-btn').click()

    cy.get('#confirm-delete-contact-btn').click({ force: true })

    cy.get('.p-toast-message .p-toast-summary')
      .should('be.visible')
      .and('contain', 'Contato excluído!')
  })
})
