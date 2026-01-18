import React from 'react';

export default function CurrentCallPanel({ apiBaseUrl, tokenProvider }) {
  return (
    <div>
      <h2>Поточний виклик</h2>
      <p>API Base URL: {apiBaseUrl}</p>
      <p>Token: (отримати через tokenProvider)</p>
    </div>
  );
}
