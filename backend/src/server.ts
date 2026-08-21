import express from 'express'
import cors from 'cors'

const app = express()

const PORT = Number(process.env.PORT) || 3000
const FRONTEND_URL = process.env.FRONTEND_URL || 'http://localhost:5173'

app.use(
  cors({
    origin: FRONTEND_URL,
  }),
)

app.use(express.json())

app.get('/api/health', (_req, res) => {
  res.json({
    status: 'ok',
    message: 'FactoryFlow API is running',
  })
})

app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`)
})