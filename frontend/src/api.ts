let token = '';
export class ApiError extends Error { constructor(message: string, public status: number) { super(message); } }
export async function refreshCsrf() {
  const response = await fetch('/api/auth/csrf', { credentials: 'same-origin' });
  if (!response.ok) throw new ApiError('Povezava s strežnikom ni uspela', response.status);
  token = (await response.json()).token;
}
export async function api<T>(path: string, body?: unknown): Promise<T> {
  if (body !== undefined && !token) await refreshCsrf();
  const response = await fetch(`/api${path}`, {
    method: body === undefined ? 'GET' : 'POST', credentials: 'same-origin',
    headers: body === undefined ? {} : { 'Content-Type': 'application/json', 'X-CSRF-TOKEN': token },
    body: body === undefined ? undefined : JSON.stringify(body),
  });
  if (!response.ok) {
    const error = await response.json().catch(() => ({}));
    throw new ApiError(error.message || (response.status === 401 ? 'Seja je potekla. Ponovno se prijavite.' : 'Zahteva ni uspela'), response.status);
  }
  return response.status === 204 ? undefined as T : response.json();
}
