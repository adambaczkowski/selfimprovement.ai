import { useNavigate } from 'react-router-dom';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import moment from 'moment';
import  './CalendarPage.scss';
const localizer = momentLocalizer(moment);

type Props = {};

const events = [
  {
    id: 1,
    title: 'Task 1',
    start: new Date(2024, 5, 9), // May 9th, 2024 (year, month (0-indexed), day)
    end: new Date(2024, 5, 9), // Can be the same date for single-day tasks
  },
  {
    id: 2,
    title: 'Task 2',
    start: new Date(2024, 5, 9), // May 10th, 2024 (year, month (0-indexed), day)
    end: new Date(2024, 5, 9), // Can be the same date for single-day tasks
  },
  // {
  //   id: 3,
  //   title: 'Task 3',
  //   start: new Date(2024, 5, 9), // May 10th, 2024 (year, month (0-indexed), day)
  //   end: new Date(2024, 5, 9), // Can be the same date for single-day tasks
  // },
  // {
  //   id: 4,
  //   title: 'Meeting 2',
  //   start: new Date(2024, 4, 9), // May 10th, 2024 (year, month (0-indexed), day)
  //   end: new Date(2024, 4, 9), // Can be the same date for single-day tasks
  // },
];


function CalendarPage({}: Props) {
  const navigate = useNavigate();

  const handleDoubleClickEvent = (event: { id: any; }) => {
    // const eventId = event.id; 
    // navigate(`/task/${eventId}`); // TODO: add id do URL
    navigate(`/task`);
  };

  return (
    <div className="background_container">
      <h1 className={"page_header"}>Calendar Page</h1>
      <Calendar
        localizer={localizer}
        events={events}
        views={['month']} // you can add: 'week', 'day'
        startAccessor="start"
        endAccessor="end"
        onDoubleClickEvent={handleDoubleClickEvent}
        style={{ height: 600 }}
      />
    </div>
  );
}

export default CalendarPage;
