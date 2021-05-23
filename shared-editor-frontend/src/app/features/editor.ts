import { InjectionToken, Pipe, PipeTransform } from '@angular/core';
import { Subject } from 'rxjs';

export interface Editor {
  text: string;
}

export const EditorSubject = new InjectionToken<Subject<Editor>>('EditorSubject');

@Pipe({
  name: 'editorText'
})
export class EditorTextPipe implements PipeTransform {
  transform(value: Editor | null): string {
    return value?.text ?? '';
  }
}