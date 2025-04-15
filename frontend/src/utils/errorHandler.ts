import axios from 'axios'

export function parseApiError(err: unknown): string {
  if (axios.isAxiosError(err)) {
    // Tenta acessar a mensagem da API
    return (
      err.response?.data?.message ??
      `Erro ${err.response?.status ?? ''} ao se comunicar com o servidor.`
    )
  }

  // fallback
  if (err instanceof Error) {
    return err.message
  }

  return 'Erro inesperado.'
}
