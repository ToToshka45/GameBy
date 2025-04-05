import Layout from "./pages/Layout";
import { Outlet } from "react-router-dom";

const App = () => {
  return (
    <main>
      <Layout>
        <Outlet />
      </Layout>
    </main>
  );
};

export default App;
