import { useEffect, useState } from 'react'

type ApiHealth = {
  status: string
  message: string
}

function App() {
  const [apiHealth, setApiHealth] = useState<ApiHealth | null>(null)

  useEffect(() => {
    const apiUrl = import.meta.env.VITE_API_URL

    fetch(`${apiUrl}/api/health`)
      .then((response) => response.json())
      .then((data: ApiHealth) => {
        setApiHealth(data)
      })
      .catch((error) => {
        console.error('Failed to connect to API:', error)
      })
  }, [])

  return (
    <main>
      <h1>FactoryFlow</h1>
      <p>Manufacturing Workflow Management System</p>

      <h2>Backend Status</h2>

      {apiHealth ? (
        <p>
          {apiHealth.status}: {apiHealth.message}
        </p>
      ) : (
        <p>Connecting to backend...</p>
      )}
    </main>
  )
}

export default App