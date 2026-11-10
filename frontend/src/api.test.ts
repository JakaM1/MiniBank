import { afterEach, expect, it, vi } from 'vitest';
import { api, ApiError, refreshCsrf } from './api';

afterEach(() => vi.unstubAllGlobals());
it('sends CSRF and same-origin credentials for mutations', async () => {
  const fetcher = vi.fn().mockResolvedValueOnce(Response.json({ token: 'csrf-test' })).mockResolvedValueOnce(new Response(null, { status: 204 }));
  vi.stubGlobal('fetch', fetcher);
  await refreshCsrf(); await api('/auth/logout', {});
  expect(fetcher.mock.calls[1]).toEqual(['/api/auth/logout', expect.objectContaining({ method: 'POST', credentials: 'same-origin', headers: { 'Content-Type': 'application/json', 'X-CSRF-TOKEN': 'csrf-test' }, body: '{}' })]);
});
it('reports expired sessions and handles non-JSON server errors', async () => {
  vi.stubGlobal('fetch', vi.fn().mockResolvedValue(new Response('', { status: 401 })));
  await expect(api('/accounts')).rejects.toMatchObject({ status: 401, message: 'Seja je potekla. Ponovno se prijavite.' });
  vi.stubGlobal('fetch', vi.fn().mockResolvedValue(new Response('unavailable', { status: 503 })));
  await expect(api('/accounts')).rejects.toBeInstanceOf(ApiError);
});
