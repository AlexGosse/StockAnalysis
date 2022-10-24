import { FetchData } from "../FetchData";
import { Home } from "../Home";
import { ChartComponent } from "../StockChart";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: "/stock-chart",
    element: <ChartComponent />,
  },
  {
    path: "/fetch-data",
    element: <FetchData />,
  },
];

export default AppRoutes;
