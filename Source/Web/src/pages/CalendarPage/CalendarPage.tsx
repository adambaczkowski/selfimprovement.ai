import { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { useQuery } from "react-query";
import { Calendar, momentLocalizer } from 'react-big-calendar';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import { fetchTasks } from "../../utils/services/goalTaskService";
import moment from 'moment';
import  './CalendarPage.scss';
const localizer = momentLocalizer(moment);

type Event = {
  id: string,
  title: string,
  start: Date,
  end: Date,
};

function CalendarPage() {
  const navigate = useNavigate();
  const [events, setEvents] = useState<Event[]>([]);

  useQuery({
    queryKey: ["getTasks"],
    queryFn: async () => {
      const tasks = await fetchTasks();
      if (tasks != null) {
        const mappedEvents: Event[] = tasks.map((task) => {
          const startDate = task.date ? new Date(task.date) : new Date();
          startDate.setMonth(startDate.getMonth() + 1);
          const endDate = new Date(startDate);

          return {
            id: task.id ?? '',
            title: task.title || 'Untitled Task',
            start: startDate,
            end: endDate,
          };
        });
        console.log(mappedEvents);
        setEvents(mappedEvents);
      }
      return tasks;
    },
    refetchOnWindowFocus: false,
  });

  const handleDoubleClickEvent = (event: { id: any; }) => {
    const eventId = event.id; 
    navigate(`/task/${eventId}`);
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
        style={{ height: 500 }}
      />
    </div>
  );
}

export default CalendarPage;
