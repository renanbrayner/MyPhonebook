export interface Contact {
  id: string
  name: string
  email: string
  phoneNumber: string
}

export interface ContactListResponse {
  contacts: Contact[]
}
